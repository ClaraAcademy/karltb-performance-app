import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";

type BankdayContextType = {
    bankday: Date | null;
    setBankday: (d: Date | null) => void;
};

const BankdayContext = createContext<BankdayContextType | undefined>(undefined);

export const BankdayProvider = ({ children }: { children: ReactNode }) => {
    const [bankday, setBankday] = useState<Date | null>(null);

    return (
        <BankdayContext.Provider value={{ bankday, setBankday }}>
            {children}
        </BankdayContext.Provider>
    );
};

export const useBankday = () => {
    const context = useContext(BankdayContext);
    if (!context) {
        throw new Error("useBankday must be used within a BankdayProvider");
    }
    return context;
};
