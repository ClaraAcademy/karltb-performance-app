import { useState } from "react";
import "./App.css";
import PortfolioDropdown from "../components/PortfolioDropdown";
import PortfolioTable from "../components/PortfolioTable";
import type { Portfolio } from "../types";


function App() {
    const [selectedPortfolio, setSelectedPortfolio] = useState<Portfolio | null>(null);

    return (
        <div>
            <h1 id="mainHeader">Performance Attribution</h1>
            <p>Choose a Portfolio</p>
            <PortfolioDropdown onSelect={setSelectedPortfolio} />
            {
                selectedPortfolio ? (
                    <p>Chosen Portfolio: {selectedPortfolio.portfolioName}</p>
                ) : (
                    <p>No Portfolio selected</p>
                )
            }
            <h2 id="tableLabel">Portfolios</h2>
            <PortfolioTable
                portfolioID={selectedPortfolio?.portfolioID}
            />
        </div>
    );
}

export default App;