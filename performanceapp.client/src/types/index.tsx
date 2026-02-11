export interface PositionColumn<T> {
  header: string;
  className: string;
  accessor: (row: T) => string;
}
export type OnChange = (e: React.ChangeEvent<HTMLSelectElement>) => void;
export type SetDate = (date: Date | null) => void;
export type SetDates = (dates: Date[]) => void;
export type SetPortfolio = (portfolio: Portfolio | null) => void;
export type SetPortfolios = (portfolios: Portfolio[]) => void;
export type SetPortfolioBenchmark = (
  portfolioBenchmark: PortfolioBenchmark | null,
) => void;
export type SetPortfolioBenchmarks = (
  portfolioBenchmarks: PortfolioBenchmark[],
) => void;
export type SetSvg = (svg: string) => void;
export type SetKeyFigures = (keyFigures: PortfolioBenchmarkKeyFigure[]) => void;

export interface LoginDto {
  username: string;
  password: string;
}

export interface KeyValue<T, S> {
  key: T;
  value: S;
}

export interface Row {
  key: string;
  values: KeyValue<string, string>[];
}

export interface Portfolio {
  portfolioId: number;
  portfolioName: string;
}

export interface PortfolioBenchmark {
  portfolioId: number;
  portfolioName: string;
  benchmarkId: number;
  benchmarkName: string;
}

export interface PortfolioBenchmarkKeyFigure {
  keyFigureId: number;
  keyFigureName: string;
  portfolioId: number;
  portfolioName: string;
  portfolioValue: number | null;
  benchmarkId: number;
  benchmarkName: string;
  benchmarkValue: number | null;
}

export interface DateInfo {
  bankday: Date;
}

export interface Position {
  portfolioId: number;
  instrumentId: number;
  instrumentName: string;
  bankday: Date;
  value: number;
  unitPrice: number;
}

export interface StockPosition extends Position {
  count: number;
}

export interface BondPosition extends Position {
  nominal: number;
}

export interface IndexPosition extends Position {
  proportion: number;
}
