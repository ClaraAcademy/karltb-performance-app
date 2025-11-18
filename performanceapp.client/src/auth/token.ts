const TOKEN = "auth_token";

export function setToken(token: string) {
    localStorage.setItem(TOKEN, token);
}

export function getToken(): string | null {
    return localStorage.getItem(TOKEN);
}

export function clearToken() {
    localStorage.removeItem(TOKEN);
}
