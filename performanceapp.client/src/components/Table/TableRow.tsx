import type { Row } from "../../types";

interface TableRowProps {
  row: Row;
}

export default function TableRow({ row }: TableRowProps) {
  return (
    <tr key={row.key}>
      {row.values.map((kv) => (
        <td key={kv.key} className={kv.key}>
          {kv.value}
        </td>
      ))}
    </tr>
  );
}
