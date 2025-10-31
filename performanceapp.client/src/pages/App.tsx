import { useState } from "react";
import "./App.css";
import PortfolioDropdown from "../components/PortfolioDropdown";
import PortfolioTable from "../components/PortfolioTable";
import type { Portfolio } from "../types";
import DateDropdown from "../components/DateDropdown";
import StockTable from "../components/StockTable";
import BondTable from "../components/BondTable";

function App() {
    const [selectedPortfolio, setSelectedPortfolio] =
        useState<Portfolio | null>(null);
    const [selectedBankday, setSelectedBankday] = useState<Date | null>(null);

    return (
        <div>
            <h1 id="mainHeader">Performance Attribution</h1>
            <PortfolioDropdown onSelect={setSelectedPortfolio} />
            <h2 id="tableLabel">Portfolios</h2>
            <PortfolioTable portfolioId={selectedPortfolio?.portfolioId} />
            <h2 id="dateHeader">Dates</h2>
            <DateDropdown onSelect={setSelectedBankday}/>
            <h2 id="stockTableHeader">Stocks</h2>
            <StockTable portfolioId={selectedPortfolio?.portfolioId} bankday={selectedBankday ?? undefined} />
            <h2 id="bondTableHeader">Bonds</h2>
            <BondTable portfolioId={selectedPortfolio?.portfolioId} bankday={selectedBankday ?? undefined} />
        </div>
    );
}

export default App;
