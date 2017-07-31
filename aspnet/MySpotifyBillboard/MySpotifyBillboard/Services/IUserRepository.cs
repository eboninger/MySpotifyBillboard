using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySpotifyBillboard.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MySpotifyBillboard.Services
{
    public interface IUserRepository
    {
        // check database for instance of user with the given spotifyId.  returns user if the user exists, 
        // otherwise returns null
        User UserExists(string spotifyId);

        // add user to our app database with the given dto. returns null if the action fails, otherwise
        // returns the new user
        Task<User> AddNewUser(SpotifyConnectionDataDto spotifyConnectionData);

        // query spotify API to get more info about the user represented in the given dto
        Task<JObject> GetUserInfo(SpotifyConnectionDataDto spotifyConnectionData);

        // update user row in user table with the given info after refresh request is made to the spotify API
        Task<User> UpdateUserAfterRefresh(User user, string accessToken, DateTime expirationTime, string scope, string tokenType);

        // call the spotify API to refresh the given user, then update database
        Task<User> Refresh(User user);

        // deletes the given user from the database
        void DeleteUser(User user);

        // update the user after scope has been changed
        User UpdateUserWithNewScope(User user, SpotifyConnectionDataDto spotifyConnectionData, string scope);

        // update user database with top track info from spotify API, return updated user
        JObject UpdateUserCharts(User user, string topTrackData, TimeFrame timeFrame);

        AddToPlaylistDto GetUrisFromUserTopTrackList(User user, TimeFrame timeFrame);
    }
}
