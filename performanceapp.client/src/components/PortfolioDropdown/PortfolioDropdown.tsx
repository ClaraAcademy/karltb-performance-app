import { type Portfolio } from "../../types";
import { createKeyValuesFromPortfolios } from "../../Factories/KeyValueFactory";
import Picker from "../Picker/Picker";

interface Props {
  portfolio: Portfolio | null;
  portfolios: Portfolio[];
  setPortfolio: (portfolio: Portfolio | null) => void;
}

export default function PortfolioPicker(props: Props) {
  const { portfolio, portfolios, setPortfolio } = props;

  const selected = portfolio?.portfolioId.toString() ?? "";
  const placeholder = "Select a portfolio";
  const values = createKeyValuesFromPortfolios(portfolios);

  const onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const id = Number(e.target.value);
    const selected = portfolios.find((p) => p.portfolioId === id) ?? null;
    setPortfolio(selected);
  };

  return (
    <Picker
      selected={selected}
      onChange={onChange}
      placeholder={placeholder}
      values={values}
    />
  );
}
