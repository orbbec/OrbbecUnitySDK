using Orbbec;

public delegate void OrbbecInitHandle();

public interface OrbbecManager
{
    void SetInitHandle(OrbbecInitHandle handle);
    bool HasInit();
    void StartStream(StreamType streamType);
    void StopStream(StreamType streamType);
    void StartAllStreams();
    void StopAllStreams();
    StreamProfile[] GetStreamProfiles(StreamType streamType);
    StreamProfile GetStreamProfile(StreamType streamType);
    void SetStreamProfile(StreamType streamType, StreamProfile profile);
    StreamData GetStreamData(StreamType streamType);

}