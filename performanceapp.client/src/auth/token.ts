const TOKEN = "auth_token";

export function setJwt(token: string) {
  localStorage.setItem(TOKEN, token);
}

export function getJwt(): string | null {
  return localStorage.getItem(TOKEN);
}

export function clearJwt() {
  localStorage.removeItem(TOKEN);
}
