describe("Login", () => {
  it("deve realizar login", () => {
    cy.visit("/");

    cy.get('input[type="email"]').type("admin@example.com");
    cy.get('input[type="password"]').type("admin123");

    cy.contains("Entrar").click();

    cy.url().should("include", "/registros");
  });

  it("deve exibir erro para credenciais inválidas", () => {
    cy.visit("/");

    cy.get('input[type="email"]').type("admin@example.com");
    cy.get('input[type="password"]').type("senhaErrada");

    cy.contains("Entrar").click();

    cy.contains("Credenciais inválidas.");
    cy.url().should("eq", Cypress.config().baseUrl + "/");
  });
});
