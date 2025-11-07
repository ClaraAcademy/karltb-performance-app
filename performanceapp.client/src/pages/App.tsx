import "./App.css";
import StockTable from "../components/StockTable";
import BondTable from "../components/BondTable";
import PortfolioGrid from "../components/PortfolioGrid";
import Header from "../components/Header";
import LineChart from "../components/LineChart";
import KeyFigureTable from "../components/KeyFigureTable";

function App() {
    return (
        <div>
            <Header />
            <PortfolioGrid />
            <h2 id="stockTableHeader">Stocks</h2>
            <StockTable />
            <h2 id="bondTableHeader">Bonds</h2>
            <BondTable />
            <h2>Line chart</h2>
            <LineChart />
            <h2>Key Figures</h2>
            <KeyFigureTable />
        </div>
    );
}

export default App;
