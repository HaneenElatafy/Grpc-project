using Grpc.Core;
using System.Threading.Tasks;
using MyGrpcService;
using Google.Protobuf.WellKnownTypes; // Assuming this is where your generated gRPC classes are

public interface ISongService
{
    Task<AddSongResponse> AddSong(AddSongRequest request, ServerCallContext context);
    Task<GetSongResponse> GetSongById(GetSongByIdRequest request, ServerCallContext context);
    Task<GetAllSongsResponse> GetAllSongs(Empty request, ServerCallContext context);
    Task<UpdateSongResponse> UpdateSong(UpdateSongRequest request, ServerCallContext context);
    Task<DeleteSongResponse> DeleteSong(DeleteSongRequest request, ServerCallContext context);
}
