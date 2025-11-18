import { getToken } from "../auth/token";

export async function api(endpoint: string, options: RequestInit = {}) {
    const token = getToken();

    const headers: HeadersInit = {
        "Content-Type": "application/json",
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
        ...options.headers,
    };

    const response = await fetch(endpoint, { ...options, headers });

    if (!response.ok) {
        throw new Error("API request failed");
    }

    return response;
}
