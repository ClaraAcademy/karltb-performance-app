import "./MainApp.css";
import StockTable from "./Positions/StockTable";
import BondTable from "./Positions/BondTable";
import PortfolioGrid from "./PortfolioGrid/PortfolioGrid";
import Header from "./Header/Header";
import LineChart from "./Charts/LineChart";
import KeyFigureTable from "./KeyFigures/KeyFigureTable";
import Report from "./Report/Report";

interface MainAppProps {
  onLogout?: () => void;
}

function MainApp({ onLogout }: MainAppProps) {
  return (
    <>
      <Header onLogout={onLogout} />
      <div className="mainContent-container">
        <div className="mainContent">
          <PortfolioGrid />
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

export default MainApp;
