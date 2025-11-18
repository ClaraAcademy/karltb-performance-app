import { setToken, clearToken } from "./token";

export async function login(username: string, password: string): Promise<string | null> {
    const response = await fetch("/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
        throw new Error("Login failed");
    }

    const data = await response.json();
    setToken(data.token);
    return data.token;
}

export function logout(): void {
    clearToken();
}
