using System.Text.Json;
using EcommerceApp.Models;
using EcommerceBlazor.Models;

namespace EcommerceApp.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://localhost:5201/api/auth"; 

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/login", loginDto);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<AuthResponseDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<ApiResponse<UserDto>> RegisterAsync(CreateUserDto createUserDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/register", createUserDto);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<UserDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<ApiResponse<UserDto>> GetProfileAsync(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{_apiBaseUrl}/profile");
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<UserDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<ApiResponse<UserDto>> UpdateProfileAsync(string token, UpdateUserDto updateUserDto)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/profile", updateUserDto);
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ApiResponse<UserDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}