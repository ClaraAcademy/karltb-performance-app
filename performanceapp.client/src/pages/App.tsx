import { useState } from "react";
import "./App.css";
import PortfolioDropdown from "../components/PortfolioDropdown";
import PortfolioTable from "../components/PortfolioTable";
import type { Portfolio } from "../types";
import DateDropdown from "../components/DateDropdown";

function App() {
    const [selectedPortfolio, setSelectedPortfolio] =
        useState<Portfolio | null>(null);

    return (
        <div>
            <h1 id="mainHeader">Performance Attribution</h1>
            <PortfolioDropdown onSelect={setSelectedPortfolio} />
            <h2 id="tableLabel">Portfolios</h2>
            <PortfolioTable portfolioID={selectedPortfolio?.portfolioID} />
            <h2 id="dateHeader">Dates</h2>
            <DateDropdown />
        </div>
    );
}

export default App;
