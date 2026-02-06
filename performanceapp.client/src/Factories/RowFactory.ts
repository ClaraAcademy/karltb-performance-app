import type { PortfolioBenchmarkKeyFigure, Row } from "../types";
import { formatPercent } from "../utilities/format";

export function createRow(kf: PortfolioBenchmarkKeyFigure): Row {
  const format = (value: number | null) => (value ? formatPercent(value) : "");
  return {
    key: kf.keyFigureId.toString(),
    values: [
      { key: "tdText", value: kf.keyFigureName },
      {
        key: "tdNumber",
        value: format(kf.portfolioValue),
      },
      {
        key: "tdNumber",
        value: format(kf.benchmarkValue),
      },
    ],
  };
}

export function createRows(kfs: PortfolioBenchmarkKeyFigure[]): Row[] {
  return kfs.map(createRow);
}
