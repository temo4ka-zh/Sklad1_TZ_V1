using Npgsql;

namespace Sklad1.Data
{
    /// <summary>
    /// Мягко добавляет таблицы и колонки, необходимые для новых модулей ТЗ, без удаления существующих данных.
    /// Выполняется через прямой Npgsql, чтобы подготовка новых таблиц не влияла на вход и регистрацию.
    /// </summary>
    public static class DatabaseSchemaInitializer
    {
        private const string ConnectionString = "Host=127.0.0.1;Database=Sklad_BD;Username=postgres;Password=milanaANDmadina";

        public static void Initialize()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
CREATE TABLE IF NOT EXISTS contractor_checks (
    id uuid PRIMARY KEY,
    inn varchar(12) NOT NULL,
    organization_name text NOT NULL DEFAULT '',
    external_status text NOT NULL DEFAULT '',
    result_status varchar(50) NOT NULL DEFAULT '',
    checked_at timestamptz NOT NULL,
    user_id uuid NOT NULL,
    document_type varchar(30) NOT NULL,
    shipment_id uuid NULL,
    supply_id uuid NULL,
    is_success boolean NOT NULL DEFAULT false,
    error_message text NULL
);

CREATE TABLE IF NOT EXISTS blacklisted_inns (
    inn varchar(12) PRIMARY KEY,
    reason text NULL,
    created_at timestamptz NOT NULL DEFAULT now()
);

CREATE TABLE IF NOT EXISTS blocked_operation_logs (
    id uuid PRIMARY KEY,
    document_type varchar(30) NOT NULL,
    inn varchar(12) NULL,
    reason text NOT NULL,
    attempted_at timestamptz NOT NULL,
    user_id uuid NOT NULL
);

CREATE TABLE IF NOT EXISTS weather_checks (
    id uuid PRIMARY KEY,
    shipment_id uuid NULL,
    city text NOT NULL,
    delivery_date date NOT NULL,
    temperature double precision NOT NULL,
    precipitation double precision NOT NULL,
    wind_speed double precision NOT NULL,
    risk_level varchar(50) NOT NULL,
    recommendation text NOT NULL,
    checked_at timestamptz NOT NULL,
    user_id uuid NOT NULL
);

ALTER TABLE product_batches ADD COLUMN IF NOT EXISTS cell_code varchar(8);
";
            command.ExecuteNonQuery();
        }
    }
}
