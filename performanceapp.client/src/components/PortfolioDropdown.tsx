import { useState, useEffect } from "react";
import type { Portfolio } from "../types";
import { usePortfolio } from "../contexts/PortfolioContext";

interface PortfolioDropdownProps {
    onSelect?: (portfolio: Portfolio | null) => void;
}

const PortfolioDropdown: React.FC<PortfolioDropdownProps> = ({ onSelect }) => {
    const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
    const { portfolio, setPortfolio } = usePortfolio();

    useEffect(() => {
        fetch("api/Portfolios")
            .then((res) => res.json())
            .then((data) => {
                console.log("Fetched portfolios:", data);
                setPortfolios(data);
            })
            .catch((err) => console.error(err));
    }, [portfolio]);

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const id = Number(e.target.value);
        const selectedPortfolio =
            portfolios.find((p) => p.portfolioId === id) ?? null;
        setPortfolio(selectedPortfolio);
        if (onSelect) {
            onSelect(portfolio);
        }
    };

    return (
        <select value={portfolio?.portfolioId ?? ""} onChange={handleChange}>
            {/* Placeholder that disappears after choice */}
            <option value="" disabled hidden>
                Select a portfolio
            </option>
            {portfolios.map((p) => (
                <option key={p.portfolioId} value={p.portfolioId}>
                    {p.portfolioName}
                </option>
            ))}
        </select>
    );
};

export default PortfolioDropdown;
