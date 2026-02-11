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
import DatePicker from "./Picker/DatePicker";

export default function MainApp() {
  const [portfolio, setPortfolio] = useState<Portfolio | null>(null);
  const [benchmark, setBenchmark] = useState<Portfolio | null>(null);
  const [date, setDate] = useState<Date | null>(null);
  return (
    <>
      <Header>
        <PortfolioGrid
          portfolio={portfolio}
          setPortfolio={setPortfolio}
          benchmark={benchmark}
          setBenchmark={setBenchmark}
        />
      </Header>
      <div className="mainContent-container">
        <div className="mainContent">
          <div className="cell" id="dateDropdown">
            <DatePicker date={date} setDate={setDate} />
          </div>
          <h2 id="stockTableHeader">Stocks</h2>
          <StockTable />
          <h2 id="bondTableHeader">Bonds</h2>
          <BondTable />
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
