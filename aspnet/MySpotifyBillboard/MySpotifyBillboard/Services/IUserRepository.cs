using MySpotifyBillboard.Models.ForSpotifyController;
using MySpotifyBillboard.Models.ForSpotifyController.Dtos;
using MySpotifyBillboard.Models.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Optional;

namespace MySpotifyBillboard.Services
{
    public interface IUserRepository
    {
        // check database for instance of user with the given spotifyId.  returns user if the user exists, 
        // otherwise returns null
        User UserExists(string spotifyId);

        // add user to our app database with the given dto. returns null if the action fails, otherwise
        // returns the new user
        Task<Option<User>> AddNewUser(SpotifyConnectionDataDto spotifyConnectionData);

        // query spotify API to get more info about the user represented in the given dto
        Task<Option<JObject>> GetUserInfo(SpotifyConnectionDataDto spotifyConnectionData);

        // update user row in user table with the given info after refresh request is made to the spotify API
        Task<User> UpdateUserAfterRefresh(User user, string accessToken, DateTime expirationTime, string scope, string tokenType);

        // call the spotify API to refresh the given user, then update database
        Task<Option<User>> Refresh(User user);

        // deletes the given user from the database
        void DeleteUser(User user);

        // update the user after scope has been changed
        User UpdateUserWithNewScope(User user, SpotifyConnectionDataDto spotifyConnectionData, string scope);

        // update user database with top track info from spotify API, return updated user
        JObject UpdateUserCharts(User user, string topTrackData, TimeFrame timeFrame);

        // return dto for adding spotify URIs to a user's playlist for the given time frame
        AddToPlaylistDto GetUrisFromUserTopTrackList(User user, TimeFrame timeFrame);

        // return true if the top track list has been updated in the past hour
        bool TTLHasBeenUpdatedRecently(User user, TimeFrame timeFrame);

        // create dto for return chart records in the given time frame to the user
        Option<JObject> CreateRecordsDto(User user, TimeFrame timeFrame);

        // create dto for return to front end of ttl belonging to given user in given time frame
        JObject CreateTopTrackListDto(User user, TimeFrame timeFrame);

        // return true if the ttl belonging to the given user in the given time frame has changed with the past twenty hours
        bool TTLHasChangedRecently(User user, TimeFrame timeFrame);

        // confirm user exists, and if the user's access token is expired refresh it.  also confirm all args
        // are non-null and have legitimate content
        Task<Option<User>> CanContinueWithRequest(Dictionary<string, string> args);

        // return true if the access token has expired, false otherwise
        bool ExpiredAccessToken(User user);

    }
}
