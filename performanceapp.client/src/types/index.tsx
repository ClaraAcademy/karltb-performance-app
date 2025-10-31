export interface Portfolio {
    portfolioId: number;
    portfolioName: string;
}

export interface PortfolioBenchmarkDTO {
    portfolioId: number;
    portfolioName: string;
    benchmarkId: number;
    benchmarkName: string;
}

export interface DateInfo {
    bankday: Date;
}
