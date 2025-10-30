import { useState, useEffect } from "react";

interface Portfolio {
    portfolioID: number;
    portfolioName: string;
}

const PortfolioDropdown = () => {
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

    return (
        <select
            value={selected?.portfolioID ?? ""}
            onChange={(e) => {
                const selectedId = Number(e.target.value);
                const selectedPortfolio = portfolios.find((p) => p.portfolioID === selectedId) ?? null;
                setSelected(selectedPortfolio)
            }}
        >
            {portfolios.map((p) => (
                <option key={p.portfolioID} value={p.portfolioID}>
                    {p.portfolioName}
                </option>
            ))}
        </select>
    );
};

export default PortfolioDropdown;