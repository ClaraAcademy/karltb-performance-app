import React, { createContext, useContext, useState, useEffect } from "react";
import type { ReactNode } from "react";
import {
    login as loginService,
    logout as logoutService,
} from "../auth/authService";
import { getJwt, setJwt, clearJwt } from "../auth/token";

interface AuthContextType {
    token: string | null;
    login: (username: string, password: string) => Promise<boolean>;
    logout: () => void;
    isAuthenticated: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({
    children,
}) => {
    const [token, setToken] = useState<string | null>(() => getJwt());

    useEffect(() => {
        if (token) {
            setJwt(token);
        } else {
            clearJwt();
        }
    }, [token]);

    const login = async (
        username: string,
        password: string,
    ): Promise<boolean> => {
        const result = await loginService(username, password);
        console.log("Login result:", result);
        if (result) {
            setToken(result);
            return true;
        }
        return false;
    };

    const logout = () => {
        setToken(null);
        clearJwt();
        logoutService();
    };

    return (
        <AuthContext.Provider
            value={{ token, login, logout, isAuthenticated: !!token }}
        >
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) throw new Error("useAuth must be used within AuthProvider");
    return context;
};
