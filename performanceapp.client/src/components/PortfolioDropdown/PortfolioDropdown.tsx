import { type Portfolio } from "../../types";
import { createKeyValuesFromPortfolios } from "../../Factories/KeyValueFactory";
import Picker from "../Picker/Picker";
import { useEffect, useState } from "react";

interface Props {
  label: string;
  portfolio: Portfolio | null;
  portfolios: Portfolio[];
  setPortfolio: (portfolio: Portfolio | null) => void;
}

export default function PortfolioPicker({
  label,
  portfolio,
  portfolios,
  setPortfolio,
}: Props) {
  const [selected, setSelected] = useState<string>("");

  useEffect(() => {
    if (portfolios.length === 1) {
      const onlyPortfolio = portfolios[0];
      setSelected(onlyPortfolio.portfolioId.toString());
      if (!portfolio || onlyPortfolio.portfolioId !== portfolio.portfolioId) {
        setPortfolio(onlyPortfolio);
      }
    } else if (portfolio) {
      setSelected(portfolio.portfolioId.toString());
    } else {
      setSelected("");
    }
  }, [portfolio, portfolios, setPortfolio]);

  const values = createKeyValuesFromPortfolios(portfolios);

  const onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const id = Number(e.target.value);
    const selected = portfolios.find((p) => p.portfolioId === id) ?? null;
    setPortfolio(selected);
  };

  return <Picker label={label} selected={selected} onChange={onChange} values={values} />;
}
