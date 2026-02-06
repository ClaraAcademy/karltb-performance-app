import ResponseCodes from "../constants/responseCodes";
import { setJwt, clearJwt } from "./token";

export async function login(
  username: string,
  password: string,
): Promise<string | null> {
  const response = await fetch("/api/auth/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });

  if (response.status === ResponseCodes.UNAUTHORIZED) {
    return null;
  }

  if (!response.ok) {
    console.error("Login failed with status:", response.status);
    return null;
  }

  const data = await response.json();
  if (data.token) {
    return data.token;
  }
  return null;
}

export function logout(): void {
  clearJwt();
}
