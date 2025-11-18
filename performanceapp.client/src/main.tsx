import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./pages/App.tsx";
import { BankdayProvider } from "./contexts/BankdayContext.tsx";
import { PortfolioProvider } from "./contexts/PortfolioContext.tsx";
import { PortfolioBenchmarkProvider } from "./contexts/PortfolioBenchmarkContext.tsx";
import { AuthProvider } from "./contexts/AuthContext.tsx";

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <AuthProvider>
            <BankdayProvider>
                <PortfolioProvider>
                    <PortfolioBenchmarkProvider>
                        <App />
                    </PortfolioBenchmarkProvider>
                </PortfolioProvider>
            </BankdayProvider>
        </AuthProvider>
    </StrictMode>,
);
