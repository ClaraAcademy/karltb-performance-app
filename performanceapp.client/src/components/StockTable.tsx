import PositionTable from "./PositionTable";
import type { StockPosition } from "../types";
import { formatSEK, formatInt } from "../utilities/format";

const stockColumns = [
    { header: "Name", accessor: (p: StockPosition) => p.instrumentName },
    { header: "Count", accessor: (p: StockPosition) => formatInt(p.count) },
    {
        header: "Daily Price",
        accessor: (p: StockPosition) => formatSEK(p.unitPrice),
    },
    { header: "Value", accessor: (p: StockPosition) => formatSEK(p.value) },
];

const StockTable = () => (
    <PositionTable<StockPosition> endpoint="stocks" columns={stockColumns} />
);

export default StockTable;
