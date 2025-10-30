import { useState, useEffect } from "react";

interface Portfolio {
    portfolioID: number;
    portfolioName: string;
}

interface PortfolioDropdownProps {
    onSelect?: (portfolio: Portfolio | null) => void;
}

const PortfolioDropdown: React.FC<PortfolioDropdownProps> = ({ onSelect }) => {
    const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
    const [selected, setSelected] = useState<Portfolio | null>(null);

    useEffect(() => {
        fetch("api/Portfolios")
            .then((res) => res.json())
            .then((data) => {
                console.log("Fetched portfolios:", data);
                setPortfolios(data);
            })
            .catch((err) => console.error(err))
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const id = Number(e.target.value);
        const selectedPortfolio = portfolios.find(
            (p) => p.portfolioID === id
        ) ?? null;
        setSelected(selectedPortfolio)
        if (onSelect) onSelect(selectedPortfolio);
    }

    return (
        <select value={selected?.portfolioID ?? ""} onChange={handleChange} >
            {portfolios.map((p) => (
                <option key={p.portfolioID} value={p.portfolioID}>
                    {p.portfolioName}
                </option>
            ))}
        </select>
    );
};

export default PortfolioDropdown;