import React, { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import MainApp from "../components/App";
import LoginForm from "../components/LoginForm";

const App: React.FC = () => {
    const { login, logout, isAuthenticated } = useAuth();
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
                <LoginForm
                    onLogin={(user, pass) => {
                        setUsername(user);
                        setPassword(pass);
                        handleLogin(
                            new Event("submit") as unknown as React.FormEvent,
                        );
                    }}
                />
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
