CREATE TABLE IF NOT EXISTS links (
  id serial PRIMARY KEY,
  short_link text NOT NULL,
  original_link text NOT NULL,
  created_at timestamptz NOT NULL DEFAULT NOW(),
  updated_at timestamptz NOT NULL DEFAULT NOW()
);

INSERT INTO links (short_link, original_link)
VALUES
-- Will get the proper shortlinks later
  ('https://go.to/abc123', 'https://example.com/original-url'),
  ('https://go.to/def456', 'https://example.com/a-really-long-path/with-multiple-directories/almost-there/but-wait-theres-more/billy-mays-here/with-more-directories-and-a-querystring?querystring=yes&moreLength=yeah&unnecessaryparam=surewhynot&gofurther=morelength&spaghetti=longnoodle&arewethereyet=yes'),
  ('https://go.to/ghi789', 'https://docs.fedoraproject.org/en-US/quick-docs/postgresql/');
