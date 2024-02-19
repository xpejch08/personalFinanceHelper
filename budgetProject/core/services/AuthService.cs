namespace core.services;

using core.interfaces;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;


public class AuthService : IAuthService
{
    //todo clean code
    public async Task<string> RegisterUser(string email, string password)
    {
        try
        {
            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs{
                    Email = email,
                    Password = password
                }
            );
            string token = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecord.Uid);
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to create a user!");
        }
    }

    public async Task<string> SignInUser(token token)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token.IdToken);
            return decodedToken.Uid;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed to sign in!");
        }
    }
}