using System;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "user.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, []);
        }
    }
    public async Task<User> AddAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;

        int maxId = users.Count > 0 ? users.Max(u => u.UserId) : 1;
        user.UserId = maxId + 1;
        users.Add(user);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;

        User? userToRemove = users.SingleOrDefault(u => u.UserId == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException(
               $"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return;
    }

    public IQueryable<User> GetMany()
    {
        string userAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        return users.AsQueryable();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;

        User? user = users.SingleOrDefault(u => u.UserId == id);
        if (user is null)
        {
            throw new InvalidOperationException(
               $"user with ID '{id}' not found");
        }
        return user;

    }

    public async Task UpdateAsync(User user)
    {
        string userAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;

        User? existingUser = users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new InvalidOperationException(
             $"User with ID {user.UserId} not found");
        }
        users.Remove(existingUser);
        users.Add(user);

        userAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, userAsJson);
        return;
    }
}
