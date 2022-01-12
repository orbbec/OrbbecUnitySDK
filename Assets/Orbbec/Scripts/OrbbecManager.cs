using Orbbec;

public interface OrbbecManager
{
    bool HasInit();
    void StartStream(StreamType streamType);
    void StopStream(StreamType streamType);
    void StartAllStream();
    void StopAllStream();
    StreamProfile[] GetStreamProfiles(StreamType streamType);
    StreamProfile GetStreamProfile(StreamType streamType);
    void SetStreamProfile(StreamType streamType, StreamProfile profile);
    StreamData GetStreamData(StreamType streamType);
}