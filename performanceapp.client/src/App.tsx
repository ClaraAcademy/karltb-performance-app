import "./App.css";
import PortfolioDropdown from "./PortfolioDropdown";
import PortfolioTable from "./PortfolioTable";

function App() {
    return (
        <div>
            <h1 id="mainHeader">Performance Attribution</h1>
            <p>Choose a Portfolio</p>
            <PortfolioDropdown />
            <h2 id="tableLabel">Portfolios</h2>
            <PortfolioTable />
        </div>
    );
}

export default App;