import "./MainApp.css";
import StockTable from "./StockTable";
import BondTable from "./BondTable";
import PortfolioGrid from "./PortfolioGrid";
import Header from "./Header";
import LineChart from "./LineChart";
import KeyFigureTable from "./KeyFigureTable";
import Report from "./Report";

function MainApp() {
    return (
        <div>
            <Header />
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
    );
}

export default MainApp;
