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
    instrumentName: number;
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
