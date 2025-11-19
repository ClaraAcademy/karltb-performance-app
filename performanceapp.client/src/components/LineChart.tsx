import { useEffect, useRef, useState } from "react";
import "./LineChart.css";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";
import { api } from "../api/api";

const LineChart = () => {
    const { portfolioBenchmark } = usePortfolioBenchmark();

    const [svg, setSvg] = useState<string | undefined>(undefined);

    const WIDTH = 800;
    const HEIGHT = 500;

    const fetchSvg = async () => {
        if (portfolioBenchmark == null) {
            return;
        }
        try {
            const portfolioId = portfolioBenchmark[0].portfolioId;
            const endpoint = `api/svg?portfolioId=${portfolioId}&width=${WIDTH}&height=${HEIGHT}`;
            const response = await api(endpoint);
            const data = await response.text();
            const encoded = "data:image/svg+xml;base64," + btoa(data);
            setSvg(encoded);
        } catch (err) {
            console.error(err);
        }
    };

    useEffect(() => {
        fetchSvg();
    }, [portfolioBenchmark]);

    return svg == null ? (
        <div className="svg" style={{ width: WIDTH, height: HEIGHT }} />
    ) : (
        <img className="svg" src={svg} alt="Chart" />
    );
};

export default LineChart;
