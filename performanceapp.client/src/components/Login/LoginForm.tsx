import React, { useState } from "react";
import "./LoginForm.css";
import LoginButton from "./LoginButton";
import LoginField from "./LoginField";
import { LoginFieldType } from "../../enums/LoginFieldType";

interface LoginFormProps {
  onLogin?: (username: string, password: string) => Promise<boolean>;
}

export default function LoginForm({ onLogin }: LoginFormProps) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!username || !password) {
      setError("Please enter both username and password.");
      return;
    }
    setError("");
    if (onLogin) {
      const success = await onLogin(username, password);
      if (!success) {
        setError("Invalid credentials");
      }
    }
  }

  return (
    <div className="login-form-container">
      <form className="login-form" onSubmit={onSubmit}>
        <h2 className="login-title">Login</h2>
        {error && <div className="login-error">{error}</div>}
        <LoginField
          id="username"
          value={username}
          label={LoginFieldType.Username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <LoginField
          id="password"
          value={password}
          label={LoginFieldType.Password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <LoginButton />
      </form>
    </div>
  );
}
