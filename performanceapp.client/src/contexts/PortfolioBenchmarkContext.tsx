import { createContext, useContext, useState } from "react";
import type { ReactNode } from "react";
import type { PortfolioBenchmark } from "../types";

type PortfolioBenchmarkContextType = {
    portfolioBenchmark: PortfolioBenchmark[] | null;
    setPortfolioBenchmark: (pb: PortfolioBenchmark[] | null) => void;
};

const PortfolioBenchmarkContext = createContext<
    PortfolioBenchmarkContextType | undefined
>(undefined);

export const PortfolioBenchmarkProvider = ({
    children,
}: {
    children: ReactNode;
}) => {
    const [portfolioBenchmark, setPortfolioBenchmark] =
        useState<PortfolioBenchmark[] | null>(null);

    return (
        <PortfolioBenchmarkContext.Provider
            value={{ portfolioBenchmark, setPortfolioBenchmark }}
        >
            {children}
        </PortfolioBenchmarkContext.Provider>
    );
};

export const usePortfolioBenchmark = () => {
    const context = useContext(PortfolioBenchmarkContext);
    if (!context) {
        throw new Error(
            "usePortfolioBenchmark must be used within a PortfolioBenchmarkProvider",
        );
    }
    return context;
};
