import "./MainApp.css";
import StockTable from "./Positions/StockTable";
import BondTable from "./Positions/BondTable";
import PortfolioGrid from "./PortfolioGrid/PortfolioGrid";
import Header from "./Header/Header";
import LineChart from "./Charts/LineChart";
import KeyFigureTable from "./KeyFigures/KeyFigureTable";
import Report from "./Report/Report";
import { useState } from "react";
import type { Portfolio } from "../types";
import Positions from "./Positions/Positions";

interface MainAppProps {
  onLogout?: () => void;
}

function MainApp({ onLogout }: MainAppProps) {
  const [portfolio, setPortfolio] = useState<Portfolio | null>(null);
  const [bankday, setBankday] = useState<Date>(new Date());
  return (
    <>
      <Header onLogout={onLogout} />
      <div className="mainContent-container">
        <div className="mainContent">
          <PortfolioGrid portfolio={portfolio} setPortfolio={setPortfolio} />
          {portfolio ? (
            <Positions portfolio={portfolio} bankday={bankday} />
          ) : null}
          <h2>Line chart</h2>
          <LineChart />
          <h2>Key Figures</h2>
          <KeyFigureTable />
          <Report />
        </div>
      </div>
    </>
  );
}

export default MainApp;
