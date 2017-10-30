﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class UserActionsDB : BaseDB
{
    public UserActionsDB(string dbConnectionName)
    {
        dbAddress = dbConnectionName;
        Connect();
    }
	private User curUser
	{
		get;
		set;
	}
    private byte[] CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);
        return buff;
    }

    public virtual byte[] SaltAndHashPassword(string password)
    {
        byte[] plainText = Encoding.ASCII.GetBytes(password);
        HashAlgorithm algorithm = new SHA256Managed();
        byte[] salt = CreateSalt(128);
        byte[] plainTextWithSaltBytes =
          new byte[password.Length + salt.Length];

        for (int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText[i];
        }
        for (int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    public bool CompareByteArrays(byte[] passHash, string entered)
    {
        byte[] enteredPass = Encoding.ASCII.GetBytes(entered);
        if (enteredPass.Length != passHash.Length)
        {
            return false;
        }

        for (int i = 0; i < passHash.Length; i++)
        {
            if (passHash[i] != enteredPass[i])
            {
                return false;
            }
        }

        return true;
    }

    public virtual async System.Threading.Tasks.Task<User> SignIn(String userName, string Password)
	{
        User user = null;
        var database = client.GetDatabase("personalshopperdb");
        var collection = database.GetCollection<BsonDocument>("users");
        using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
        {
            while (await cursor.MoveNextAsync())
            {
                IEnumerable<BsonDocument> batch = cursor.Current;
                foreach (BsonDocument document in batch)
                {
                    /*if (document.Contains(userName)) {
                        string[] data = document.ToString().Split(',');
                        foreach (string d in data)
                        {
                            if (d.Contains("passhash"))
                            {
                                d.Replace("\"passhash\" : [", "");
                                d.Replace("]", "");
                                byte[] passHash;
                                CompareByteArrays()
                            }
                        }
                    }*/
                    //[ :]+((?=\[)\[[^]]*\]|(?=\{)\{[^\}]*\}|\"[^"]*\") regex for bson/json
                }
            }
        }
        return user;
    }

	public virtual bool CreateUser(User  user)
	{
		throw new System.NotImplementedException();
	}

	public virtual bool EditUser(User user)
	{
		throw new System.NotImplementedException();
	}

}

