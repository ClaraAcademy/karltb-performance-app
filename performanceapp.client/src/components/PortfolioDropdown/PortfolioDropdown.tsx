import { useState, useEffect } from "react";
import type { Portfolio } from "../../types";
import { usePortfolio } from "../../contexts/PortfolioContext";
import { api } from "../../api/api";

interface PortfolioDropdownProps {
  onSelect?: (portfolio: Portfolio | null) => void;
}

const PortfolioDropdown: React.FC<PortfolioDropdownProps> = ({ onSelect }) => {
  const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
  const { portfolio, setPortfolio } = usePortfolio();

  const fetchPortfolios = async () => {
    try {
      const response = await api("api/Portfolio");
      const data: Portfolio[] = await response.json();
      setPortfolios(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchPortfolios();
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
