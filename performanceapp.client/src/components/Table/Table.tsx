import type { Row } from "../../types";
import TableHeader from "./TableHeader";
import TableRow from "./TableRow";

interface TableProps {
  header: Row;
  rows: Row[];
}

export default function Table({ header, rows }: TableProps) {
  return (
    <table
      className="table table-striped"
      aria-labelledby="keyFigureTableLabel"
    >
      <TableHeader header={header} />
      <tbody>
        {rows.map((row) => (
          <TableRow row={row} />
        ))}
      </tbody>
    </table>
  );
}
