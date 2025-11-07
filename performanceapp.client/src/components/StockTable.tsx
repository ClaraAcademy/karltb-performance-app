import PositionTable from "./PositionTable";
import type { StockPosition } from "../types";
import { formatSEK, formatInt } from "../utilities/format";

const stockColumns = [
    {
        header: "Name",
        className: "tdText",
        accessor: (p: StockPosition) => p.instrumentName,
    },
    {
        header: "Count",
        className: "tdNumber",
        accessor: (p: StockPosition) => formatInt(p.count),
    },
    {
        header: "Daily Price",
        className: "tdNumber",
        accessor: (p: StockPosition) => formatSEK(p.unitPrice),
    },
    {
        header: "Value",
        className: "tdNumber",
        accessor: (p: StockPosition) => formatSEK(p.value),
    },
];

const StockTable = () => (
    <PositionTable<StockPosition> endpoint="stocks" columns={stockColumns} />
);

export default StockTable;
