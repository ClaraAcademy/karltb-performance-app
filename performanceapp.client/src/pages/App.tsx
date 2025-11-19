import React, { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import MainApp from "../components/MainApp";
import LoginForm from "../components/LoginForm";

const App: React.FC = () => {
    const { login, logout, isAuthenticated } = useAuth();

    const handleLogin = async (
        username: string,
        password: string,
    ): Promise<boolean> => {
        const success = await login(username, password);
        return success;
    };

    const handleLogout = () => {
        logout();
    };

    return (
        <div>
            {!isAuthenticated ? (
                <LoginForm onLogin={handleLogin} />
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
