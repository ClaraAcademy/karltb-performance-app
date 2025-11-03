import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./pages/App.tsx";
import { BankdayProvider } from "./contexts/BankdayContext.tsx";
import { PortfolioProvider } from "./contexts/PortfolioContext.tsx";
import { PortfolioBenchmarkProvider } from "./contexts/PortfolioBenchmarkContext.tsx";

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <BankdayProvider>
            <PortfolioProvider>
                <PortfolioBenchmarkProvider>
                    <App />
                </PortfolioBenchmarkProvider>
            </PortfolioProvider>
        </BankdayProvider>
    </StrictMode>,
);
