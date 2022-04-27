-- Idade maior que cinquenta agrupado por condominio *** Falha na montagem do sql ***
SELECT dbo.condominio.nome as cond_nome, count(dbo.familia.id_condominio) as contagem, 
dbo.morador.nome as morador_nome, idade, dbo.familia.id_condominio FROM dbo.morador 
INNER JOIN dbo.familia ON id_familia = dbo.familia.id
INNER JOIN dbo.condominio ON dbo.familia.id_condominio = dbo.condominio.id
where dbo.morador.idade > 50 
group by dbo.condominio.id, dbo.morador.nome, dbo.morador.idade, 
dbo.familia.id_condominio, dbo.condominio.nome


-- Relatório de moradores por condomínio
SELECT dbo.morador.nome as pessoa, dbo.familia.nome as familia,
dbo.condominio.nome as condominio FROM dbo.morador 
INNER JOIN dbo.familia ON id_familia = dbo.familia.id
INNER JOIN dbo.condominio on dbo.condominio.id = dbo.familia.id_condominio
Group by dbo.familia.id_condominio, dbo.morador.nome, dbo.familia.nome, 
dbo.condominio.nome

-- Média de idade de morador por bairro
SELECT  dbo.condominio.bairro, avg(dbo.morador.idade) as idade_media FROM dbo.morador 
INNER JOIN dbo.familia ON id_familia = dbo.familia.id
INNER JOIN dbo.condominio on dbo.condominio.id = dbo.familia.id_condominio
Group by dbo.condominio.bairro

