import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";
import type { Portfolio } from "../types";

type PortfolioContextType = {
  portfolio: Portfolio | null;
  setPortfolio: (p: Portfolio | null) => void;
};

const PortfolioContext = createContext<PortfolioContextType | undefined>(
  undefined,
);

export const PortfolioProvider = ({ children }: { children: ReactNode }) => {
  const [portfolio, setPortfolio] = useState<Portfolio | null>(null);

  return (
    <PortfolioContext.Provider value={{ portfolio, setPortfolio }}>
      {children}
    </PortfolioContext.Provider>
  );
};

export const usePortfolio = () => {
  const context = useContext(PortfolioContext);
  if (!context) {
    throw new Error("usePortfolio must be used within a PortfolioProvider");
  }
  return context;
};
