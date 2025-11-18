import React, { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import MainApp from "../components/App";

const App: React.FC = () => {
    const { token, login, logout, isAuthenticated } = useAuth();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        const success = await login(username, password);
        if (!success) setError("Invalid credentials");
        else setError("");
    };

    const handleLogout = () => {
        logout();
    };

    return (
        <div>
            {!isAuthenticated ? (
                <form onSubmit={handleLogin}>
                    <input
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Username"
                    />
                    <input
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        type="password"
                        placeholder="Password"
                    />
                    <button type="submit">Login</button>
                    {error && <div style={{ color: "red" }}>{error}</div>}
                </form>
            ) : (
                <div>
                    <button onClick={handleLogout}>Logout</button>
                    {/* Render your main app content here */}
                    <MainApp />
                </div>
            )}
        </div>
    );
};

export default App;
