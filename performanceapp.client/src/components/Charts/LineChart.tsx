import { HEIGHT, WIDTH } from "../../enums/SvgDimensions";
import { useEffect, useState } from "react";
import "./LineChart.css";
import { fetchAndSetLineChart } from "../../api/FetchLineChart";
import Svg from "../Svg/Svg";

interface LineChartProps {
  portfolioId: number | undefined;
}

export default function LineChart({ portfolioId }: LineChartProps) {
  const [svg, setSvg] = useState("");

  useEffect(() => {
    portfolioId
      ? fetchAndSetLineChart(portfolioId, WIDTH, HEIGHT, setSvg)
      : setSvg("");
  }, [portfolioId]);

  return (
    <>
      <h2>Performance Line Chart</h2>
      <Svg content={svg} />;
    </>
  );
}
