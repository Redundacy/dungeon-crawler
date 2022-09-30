using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An example network serializer with both server and owner authority.
/// Love Tarodev
/// </summary>
public class PlayerTransform : NetworkBehaviour {
    /// <summary>
    /// A toggle to test the difference between owner and server auth.
    /// </summary>
    [SerializeField] private bool _serverAuth;
    [SerializeField] private float _cheapInterpolationTime = 0.1f;

    private NetworkVariable<PlayerNetworkState> _playerState;
    //private PointsHandler _pointsHandler;
    private bool isReady = false;

    private void Awake()
    {
        //_pointsHandler = transform.GetComponent<PointsHandler>();

        var permission = _serverAuth ? NetworkVariableWritePermission.Server : NetworkVariableWritePermission.Owner;
        _playerState = new NetworkVariable<PlayerNetworkState>(writePerm: permission);
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner) transform.Find("UserPanel").gameObject.SetActive(false);
        else transform.Find("OpponentPanel").gameObject.SetActive(false);
    }

    private void Update()
    {
        // instead of sending information every frame, I can send information when actions are made
        // (In this specific game, only when someone wants to submit a stat change.)
        // if (IsOwner) TransmitState();
        // else ConsumeState();
    }

    public void UpdateState()
    {
        if (!IsOwner) ConsumeState();
    }

    public bool GetNetworkReady()
    {
        return _playerState.Value.PlayerStats.Item5;
    }

    #region Transmit State

    public void TransmitState() {
        var state = new PlayerNetworkState {
            //PlayerStats = (_pointsHandler.HPPoints, _pointsHandler.SpeedPoints, _pointsHandler.AttackPoints, _pointsHandler.remainingPoints, true)
        };

        if (IsServer || !_serverAuth)
        {
            _playerState.Value = state;
        }
        else
            TransmitStateServerRpc(state);
        Debug.Log("Transmitted Data.");
    }

    [ServerRpc]
    private void TransmitStateServerRpc(PlayerNetworkState state) {
        _playerState.Value = state;
    }

    #endregion

    #region Interpolate State
    
    public void ConsumeState()
    {
        transform.Find("OpponentPanel").Find("OpponentHP").GetComponent<TextMeshProUGUI>().text = _playerState.Value.PlayerStats.Item1 + "";
        transform.Find("OpponentPanel").Find("OpponentSpeed").GetComponent<TextMeshProUGUI>().text = _playerState.Value.PlayerStats.Item2 + "";
        transform.Find("OpponentPanel").Find("OpponentAttack").GetComponent<TextMeshProUGUI>().text = _playerState.Value.PlayerStats.Item3 + "";
    }

    #endregion

    private struct PlayerNetworkState : INetworkSerializable
    {
        private int HPValue;
        private int SpeedValue;
        private int AttackValue;
        private int PointsRemaining;
        private bool Ready;
        internal (int, int, int, int, bool) PlayerStats
        {
            get => (HPValue, SpeedValue, AttackValue, PointsRemaining, Ready);
            set
            {
                (HPValue, SpeedValue, AttackValue, PointsRemaining, Ready) = value;
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            serializer.SerializeValue(ref HPValue);
            serializer.SerializeValue(ref SpeedValue);
            serializer.SerializeValue(ref AttackValue);
            serializer.SerializeValue(ref PointsRemaining);

            serializer.SerializeValue(ref Ready);
        }
    }
}