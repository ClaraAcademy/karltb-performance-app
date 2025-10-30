import { useState, useEffect } from "react";
import "./App.css";

interface Portfolio {
    portfolioId: number;
    portfolioName: string;
}

function App() {
    const [portfolios, setPortfolios] = useState<Portfolio[]>([]);

    useEffect(() => {
        fetch("api/Portfolios")
            .then((res) => {
                if (!res.ok) throw new Error(res.statusText);
                return res.json();
            })
            .then((data) => {
                console.log("Fetched portfolios:", data);
                setPortfolios(data);
            })
            .catch((err) => console.error("Fetch error:", err));
    }, []);

    const contents = portfolios === undefined
        ? <p><em>LOADING</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {portfolios.map(portfolio =>
                    <tr key={portfolio.portfolioId}>
                        <td>{portfolio.portfolioId}</td>
                        <td>{portfolio.portfolioName}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Portfolios</h1>
            {contents}
        </div>
    );
}

export default App;