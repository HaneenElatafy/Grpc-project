using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using MyGrpcService; // Assuming the generated classes from the proto file are in this namespace
using System.Models; // For Song model
using System.Data;
using system.context;
using Google.Protobuf.WellKnownTypes; // For SongContext

public class SongServiceImplementation : SongService.SongServiceBase
{
    private readonly SongContext _context;

    public SongServiceImplementation(SongContext context)
    {
        _context = context;
    }

    // Add a new song
    public override async Task<AddSongResponse> AddSong(AddSongRequest request, ServerCallContext context)
    {
        var newSong = new Song
        {
            Name = request.Name,
            Artist = request.Artist
        };

        _context.Songs.Add(newSong);

        try
        {
            await _context.SaveChangesAsync();
            return new AddSongResponse
            {
                Success = true,
                Message = "Song added successfully."
            };
        }
        catch (Exception ex)
        {
            return new AddSongResponse
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    // Get a song by its ID
    public override async Task<GetSongResponse> GetSongById(GetSongByIdRequest request, ServerCallContext context)
    {
        var song = await _context.Songs.FindAsync(request.Id);

        if (song == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Song not found"));
        }

        return new GetSongResponse
        {
            Id = song.Id,
            Name = song.Name,
            Artist = song.Artist
        };
    }

    // Get all songs
    public override async Task<GetAllSongsResponse> GetAllSongs(Empty request, ServerCallContext context)
    {
        var songs = await _context.Songs.ToListAsync();

        var response = new GetAllSongsResponse();

        foreach (var song in songs)
        {
            response.Songs.Add(new GetSongResponse
            {
                Id = song.Id,
                Name = song.Name,
                Artist = song.Artist
            });
        }

        return response;
    }

    // Update an existing song
    public override async Task<UpdateSongResponse> UpdateSong(UpdateSongRequest request, ServerCallContext context)
    {
        var song = await _context.Songs.FindAsync(request.Id);

        if (song == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Song not found"));
        }

        song.Name = request.Name;
        song.Artist = request.Artist;

        try
        {
            await _context.SaveChangesAsync();
            return new UpdateSongResponse
            {
                Success = true,
                Message = "Song updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new UpdateSongResponse
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }

    // Delete a song by its ID
    public override async Task<DeleteSongResponse> DeleteSong(DeleteSongRequest request, ServerCallContext context)
    {
        var song = await _context.Songs.FindAsync(request.Id);

        if (song == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Song not found"));
        }

        _context.Songs.Remove(song);

        try
        {
            await _context.SaveChangesAsync();
            return new DeleteSongResponse
            {
                Success = true,
                Message = "Song deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new DeleteSongResponse
            {
                Success = false,
                Message = $"An error occurred: {ex.Message}"
            };
        }
    }
}
