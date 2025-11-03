import { useState } from "react";
import "./App.css";
import PortfolioDropdown from "../components/PortfolioDropdown";
import PortfolioTable from "../components/PortfolioTable";
import DateDropdown from "../components/DateDropdown";
import StockTable from "../components/StockTable";
import BondTable from "../components/BondTable";
import { useBankday } from "../contexts/BankdayContext";
import { usePortfolio } from "../contexts/PortfolioContext";

function App() {
    return (
        <div>
            <h1 id="mainHeader">Performance Attribution</h1>
            <PortfolioDropdown />
            <h2 id="tableLabel">Portfolios</h2>
            <PortfolioTable />
            <h2 id="dateHeader">Dates</h2>
            <DateDropdown />
            <h2 id="stockTableHeader">Stocks</h2>
            <StockTable />
            <h2 id="bondTableHeader">Bonds</h2>
            <BondTable />
        </div>
    );
}

export default App;
