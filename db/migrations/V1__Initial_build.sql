CREATE TABLE
  IF NOT EXISTS links (
    id serial PRIMARY KEY,
    short_link text NOT NULL,
    long_url text NOT NULL,
    created_at timestamptz NOT NULL DEFAULT NOW (),
    updated_at timestamptz NOT NULL DEFAULT NOW (),
    CONSTRAINT unq_short_link UNIQUE (short_link)
  );

CREATE INDEX IF NOT EXISTS idx_short_link on links(short_link);
CREATE INDEX IF NOT EXISTS idx_long_url on links(long_url);